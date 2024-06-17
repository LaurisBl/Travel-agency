import { Arg, Query, Resolver } from "type-graphql";
import {
	Baggage,
	TransportationType,
	TransportationRent
} from "../../schema";

@Resolver()
export default class TransportationQuery {
    @Query(() => [Baggage])
    async getBaggages() {
        return await Baggage.find();
    }

    @Query(() => Baggage) 
    async getBaggage(@Arg('baggageID') baggageID: number) {
        return await Baggage.findOneBy({
            baggageID: baggageID
        });
    }

    @Query(() => [Baggage])
    async findBaggage(@Arg('tripID') tripID: number) {
        return await Baggage.findBy({
            tripID: tripID
        })
    }

    @Query(() => [TransportationType])
    async getTransportationTypes() {
        return await TransportationType.find();
    }

    @Query(() => TransportationType) 
    async getTransportationType(@Arg('transportationTypeID') transportationTypeID: number) {
        return await TransportationType.findOneBy({
            transportationTypeID: transportationTypeID
        });
    }

    @Query(() => [TransportationRent])
    async getTransportationRentList() {
        return await TransportationRent.find();
    }

    @Query(() => TransportationRent) 
    async getTransportationRent(@Arg('transportationRentID') transportationRentID: number) {
        return await TransportationRent.findOneBy({
            transportationRentID: transportationRentID
        });
    }
}