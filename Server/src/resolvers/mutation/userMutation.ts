import { Arg, Ctx, Mutation, Resolver } from "type-graphql";
import log from "fancy-log";
import Context from "../../types/context";
import { ApolloError } from "apollo-server";
import { signJwt } from "../../utils/jwt";
import {
    BaseUser,
	User,
	Admin,
	Consultant,
	Guide
} from "../../schema";


@Resolver()
export default class UserMutation {
    @Mutation(() => String)
    async login(
        @Arg('eMail') email: string, 
        @Arg('passwordKey') pass: string, 
        @Ctx() context: Context
    ) {
        var user : BaseUser | null = await User.findOneBy({
            eMail: email,
            passwordKey: pass
        });

        if(!user)
            user = await Admin.findOneBy({
                eMail: email,
                passwordKey: pass
            });

            if(!user)
                user = await Guide.findOneBy({
                    eMail: email,
                    passwordKey: pass
                });
    
                if(!user)
                    user = await Consultant.findOneBy({
                        eMail: email,
                        passwordKey: pass
                    });
        
                if(!user) {
                    log.error("User does not exist!");
                    throw new ApolloError("User does not exist!");
                }

        user.setID();

        const token = signJwt(user as object);

        return token;
    }

    @Mutation(() => User)
    async createUser(
        @Arg('fName') fName: string,
        @Arg('eMail') eMail: string,
        @Arg('passwordKey') passwordKey: string
    ) {
        var result = await User.insert({fName, eMail, passwordKey, phone:""});

        return await User.findOneBy(
            { 
                userID: result.identifiers[0].userID
            }
        );
    }

    @Mutation(() => Admin)
    async createAdmin(
        @Arg('fName') fName: string,
        @Arg('eMail') eMail: string,
        @Arg('passwordKey') passwordKey: string
    ) {
        var result = await Admin.insert({fName, eMail, passwordKey});

        return await Admin.findOneBy(
            { 
                adminID: result.identifiers[0].adminID
            }
        );
    }

    @Mutation(() => Consultant)
    async createConsultant(
        @Arg('fName') fName: string,
        @Arg('eMail') eMail: string,
        @Arg('passwordKey') passwordKey: string
    ) {
        var result = await Consultant.insert({fName, eMail, passwordKey, phone:""});

        return await Consultant.findOneBy(
            { 
                consultantID: result.identifiers[0].consultantID
            }
        );
    }

    @Mutation(() => Guide)
    async createGuide(
        @Arg('fName') fName: string,
        @Arg('eMail') eMail: string,
        @Arg('passwordKey') passwordKey: string
    ) {
        var result = await Guide.insert({fName, eMail, passwordKey, phone:""});

        return await Guide.findOneBy(
            { 
                guideID: result.identifiers[0].guideID
            }
        );
    }
}