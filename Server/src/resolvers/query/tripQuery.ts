import { Arg, Query, Resolver } from "type-graphql";
import {
	Review,
	TravelParty,
	Trip,
	Price
} from "../../schema";

@Resolver()
export default class TripQuery {
    @Query(() => [TravelParty])
    async getTravelParties() {
        return await TravelParty.find();
    }

    @Query(() => TravelParty) 
    async getTravelParty(@Arg('travelPartyID') travelPartyID: number) {
        return await TravelParty.findOneBy({
            travelPartyID: travelPartyID
        });
    }

    @Query(() => TravelParty) 
    async findTravelParty(@Arg('tripID') tripID: number) {
        return await TravelParty.findOneBy({
            tripID: tripID
        });
    }

    @Query(() => [Trip])
    async getTrips() {
        return await Trip.find();
    }

    @Query(() => Trip) 
    async getTrip(@Arg('tripID') tripID: number) {
        return await Trip.findOneBy({
            tripID: tripID
        });
    }

    @Query(() => [Trip])
    async findTrips(@Arg('userID') userID: number) {
        return await Trip.findBy({
            userID: userID
        });
    }

    @Query(() => [Trip])
    async findTripsForGuide(@Arg('guideID') guideID: number) {
        return await Trip.findBy({
            guideID: guideID
        });
    }

    @Query(() => [Trip])
    async findTripsForConsultant(@Arg('consultantID') consultantID: number) {
        return await Trip.findBy({
            consultantID: consultantID
        });
    }

    @Query(() => Price) 
    async getPrice(@Arg('priceID') priceID: number) {
        return await Price.findOneBy({
            priceID: priceID
        });
    }

    @Query(() => Price) 
    async findPrice(@Arg('tripID') tripID: number) {
        return await Price.findOneBy({
            tripID: tripID
        });
    }

    @Query(() => [Review])
    async getReviews() {
        return await Review.find();
    }

    @Query(() => Review) 
    async getReview(@Arg('reviewID') reviewID: number) {
        return await Review.findOneBy({
            reviewID: reviewID
        });
    }

    @Query(() => Review) 
    async findReview(@Arg('tripID') tripID: number) {
        return await Review.findOneBy({
            tripID: tripID
        });
    }
}