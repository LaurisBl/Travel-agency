import { Arg, Ctx, Mutation, Resolver } from "type-graphql";
import log from "fancy-log";
import Context from "../../types/context";
import { ApolloError } from "apollo-server";
import { signJwt } from "../../utils/jwt";
import {
	Review,
	TravelParty,
	Trip,
	Price
} from "../../schema";


@Resolver()
export default class TripMutation {
    @Mutation(() => Trip) 
    async createTrip(
        @Arg('userID') userID: number,
        @Arg('name') name: string,
        @Arg('guideID') guideID: number,
        @Arg('consultantID') consultantID: number,
        @Arg('dateTimeID') dateTimeID: number,
        @Arg('transportRentID') transportRentID: number
    ) {
        var result = await Trip.insert({ 
            userID: userID,
            name: name,
            guideID: guideID,
            consultantID: consultantID,
            dateTimeID: dateTimeID,
            transportRentID: transportRentID,
        });

        return await Trip.findOneBy(
            { 
                tripID: result.identifiers[0].tripID
            }
        );
    }

    @Mutation(() => Review)
    async createReview(
        @Arg('tripID') tripID: number,
        @Arg('rating') rating: number,
        @Arg('description') description: string,
    ) {
        var result = await Review.insert({ 
            tripID: tripID,
            rating: rating,
            description: description
        });

        return await Review.findOneBy(
            { 
                reviewID: result.identifiers[0].reviewID
            }
        );
    }

    @Mutation(() => TravelParty)
    async addTravelParty(
        @Arg('tripID') tripID: number,
        @Arg('amount') amount: number
    ) {
        var result = await TravelParty.insert({
            tripID: tripID,
            amount: amount
        });

        return await TravelParty.findOneBy({
            travelPartyID: result.identifiers[0].travelPartyID
        });
    }

    @Mutation(() => Price)
    async setPrice(
        @Arg('tripID') tripID: number,
        @Arg('price') price: number
    ) {
        var result = await Price.insert({
            tripID: tripID,
            price: price
        });

        return await Price.findOneBy({
            priceID: result.identifiers[0].priceID
        });
    }
}