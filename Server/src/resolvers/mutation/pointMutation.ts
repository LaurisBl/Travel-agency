import { Arg, Ctx, Mutation, Resolver } from "type-graphql";
import log from "fancy-log";
import Context from "../../types/context";
import { ApolloError } from "apollo-server";
import { signJwt } from "../../utils/jwt";
import {
	Point,
	PointType,
	TripPoint
} from "../../schema";


@Resolver()
export default class PointMutation {
    @Mutation(() => TripPoint)
    async addPointToTrip(
        @Arg('pointID') pointID: number,
        @Arg('tripID') tripID: number
    ) {
        var result = await TripPoint.insert({ pointID: pointID, tripID: tripID });

        return await TripPoint.findOneBy(
            { 
                tripPointID: result.identifiers[0].tripPointID
            }
        );
    }

    @Mutation(() => Point)
    async createPoint(
        @Arg('pointTypeID') pointTypeID: number,
        @Arg('cityID') cityID: number,
        @Arg('name') name: string,
        @Arg('address') address: string,
        @Arg('price') price: number,
    ) {
        var result = await Point.insert({ 
            pointTypeID: pointTypeID,
            cityID: cityID,
            name: name,
            address: address,
            price: price
        });

        return await Point.findOneBy(
            { 
                pointID: result.identifiers[0].pointID
            }
        );
    }

    @Mutation(() => String)
    async deletePoint(
        @Arg('pointID') pointID: number
    ) {
        var result = await Point.delete({ pointID: pointID });

        return ((result.affected ?? 0) != 0 ? "OK" : "NOT_FOUND");
    }
}