import { Arg, Ctx, Mutation, Resolver } from "type-graphql";
import log from "fancy-log";
import Context from "../../types/context";
import { ApolloError } from "apollo-server";
import { signJwt } from "../../utils/jwt";
import {
	City,
	Country,
	Image,
	DateTime
} from "../../schema";


@Resolver()
export default class EtcMutation {
    @Mutation(() => Country)
    async createCountry(
        @Arg('name') name: string
    ) {
        var result = await Country.insert({name});

        return await Country.findOneBy(
            { 
                countryID: result.identifiers[0].countryID
            }
        );
    }

    @Mutation(() => String)
    async deleteCountry(
        @Arg('countryID') countryID: number
    ) {
        var result = await Country.delete(
            {
                countryID: countryID
            }
        );

        return ((result.affected ?? 0) != 0 ? "OK" : "NOT_FOUND");
    }

    @Mutation(() => City)
    async createCity(
        @Arg('name') name: string,
        @Arg('countryID') countryID: number
    ) {
        var result = await City.insert({name, countryID});

        return await City.findOneBy(
            { 
                cityID: result.identifiers[0].cityID
            }
        );
    }

    @Mutation(() => String!)
    async deleteCity(
        @Arg('cityID') cityID: number
    ) {
        var result = await City.delete(
            {
                cityID: cityID
            }
        );
        
        return ((result.affected ?? 0) != 0 ? "OK" : "NOT_FOUND");
    }

    @Mutation(() => Image)
    async addImage(
        @Arg('pointID') pointID: number,
        @Arg('imageLink') imageLink: string
    ) {
        var result = await Image.insert({ pointID: pointID, imageLink: imageLink });

        return await Image.findOneBy(
            { 
                imageID: result.identifiers[0].imageID
            }
        );
    }

    @Mutation(() => String)
    async removeImagesForPoint(
        @Arg('pointID') pointID: number
    ) {
        var result = await Image.delete(
            {
                pointID: pointID
            }
        );
        
        return ((result.affected ?? 0) != 0 ? "OK" : "NOT_FOUND");
    }

    @Mutation(() => DateTime)
    async addDateTime(@Arg('timeStamp') timeStamp: string) {
        var result = await DateTime.insert({ timeStamp: timeStamp });

        return await DateTime.findOneBy(
            { 
                dateTimeID: result.identifiers[0].dateTimeID
            }
        );
    }
}