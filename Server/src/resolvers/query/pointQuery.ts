import { Arg, Query, Resolver } from "type-graphql";
import {
	Point,
	PointType,
	TripPoint
} from "../../schema";

@Resolver()
export default class PointQuery {
    @Query(() => [PointType]!)
    async getPointTypes() {
        return await PointType.find();
    }

    @Query(() => [Point])
    async getPoints() {
        return await Point.find();
    }

    @Query(() => Point) 
    async getPoint(@Arg('pointID') pointID: number) {
        return await Point.findOneBy({
            pointID: pointID
        });
    }

    @Query(() => [TripPoint]) 
    async findPointsForTrip(@Arg('tripID') tripID: number) {
        return await TripPoint.findBy({
            tripID: tripID
        });
    }

    @Query(() => [TripPoint]) 
    async getTripPoints(@Arg('tripID') tripID: number) {
        return await TripPoint.findBy({
            tripID: tripID
        });
    }
}