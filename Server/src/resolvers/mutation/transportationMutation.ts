import { Arg, Ctx, Mutation, Resolver } from "type-graphql";
import log from "fancy-log";
import Context from "../../types/context";
import { ApolloError } from "apollo-server";
import { signJwt } from "../../utils/jwt";
import {
	Baggage,
	TransportationRent
} from "../../schema";


@Resolver()
export default class TransportationMutation {
	@Mutation(() => Baggage) 
	async addBaggage(
		@Arg('tripID') tripID: number,
		@Arg('transportTypeID') transportTypeID: number,
		@Arg('amount') amount: number,
		@Arg('weight') weight: number
	) {
		var result = await Baggage.insert({ 
			tripID: tripID,
			transportTypeID: transportTypeID,
			amount: amount,
			weight: weight,
		});

        return await Baggage.findOneBy(
            { 
                baggageID: result.identifiers[0].baggageID
            }
        );
	}

	@Mutation(() => TransportationRent) 
	async addTransportationRent(
		@Arg('countryID') countryID: number,
		@Arg('transportationTypeID') transportationTypeID: number,
		@Arg('companyName') companyName: string,
		@Arg('price') price: number
	) {
		var result = await TransportationRent.insert({ 
			countryID: countryID,
			transportationTypeID: transportationTypeID,
			companyName: companyName,
			price: price,
		});

        return await TransportationRent.findOneBy(
            { 
                transportationRentID: result.identifiers[0].transportationRentID
            }
        );
	}

	@Mutation(() => String)
	async removeTransportationRent(@Arg('transportationRentID') transportationRentID: number) {
		var result = await TransportationRent.delete({ transportationRentID: transportationRentID });

        return ((result.affected ?? 0) != 0 ? "OK" : "NOT_FOUND");
	}
}