import { Arg, Ctx, Query, Resolver } from "type-graphql";
import { verifyJwt } from "../../utils/jwt";
import Context from "../../types/context";
import { ApolloError } from "apollo-server";
import {
    BaseUser,
	User,
	Admin,
	Consultant,
	Guide
} from "../../schema";

@Resolver()
export default class UserQuery {
    @Query(() => BaseUser!)
	me(@Ctx() ctx: Context) {
		if(!ctx.req.headers.token)
			throw new ApolloError("User not authenticated");

		return verifyJwt<BaseUser>(ctx.req.headers.token as string);
	}

	@Query(() => [User])
    async getUsers() {
        return await User.find();
    }

    @Query(() => User) 
    async getUser(@Arg('userID') userID: number) {
        return await User.findOneBy({
            userID: userID
        });
    }

    @Query(() => Admin) 
    async getAdmin(@Arg('adminID') adminID: number) {
        return await Admin.findOneBy({
            adminID: adminID
        });
    }

    @Query(() => [Admin]) 
    async getAdmins() {
        return await Admin.find();
    }

    @Query(() => [Guide])
    async getGuides() {
        return await Guide.find();
    }

    @Query(() => Guide) 
    async getGuide(@Arg('guideID') guideID: number) {
        return await Guide.findOneBy({
            guideID: guideID
        });
    }

    @Query(() => [Consultant])
    async getConsultants() {
        return await Consultant.find();
    }

    @Query(() => Consultant) 
    async getConsultant(@Arg('consultantID') consultantID: number) {
        return await Consultant.findOneBy({
            consultantID: consultantID
        });
    }
}