import { Arg, Query, Resolver } from "type-graphql";
import {
	City,
	Country,
	Image,
	DateTime
} from "../../schema";

@Resolver()
export default class EtcQuery {
    @Query(() => [City])
    async getCities() {
        return await City.find();
    }

    @Query(() => City) 
    async getCity(@Arg('cityID') cityID: number) {
        return await City.findOneBy({
            cityID: cityID
        });
    }

    @Query(() => [Country])
    async getCountries() {
        return await Country.find();
    }

    @Query(() => Country) 
    async getCountry(@Arg('countryID') countryID: number) {
        return await Country.findOneBy({
            countryID: countryID
        });
    }

    @Query(() => [Image])
    async getImages() {
        return await Image.find();
    }

    @Query(() => Image) 
    async getImage(@Arg('imageID') imageID: number) {
        return await Image.findOneBy({
            imageID: imageID
        });
    }

    @Query(() => [Image]!) 
    async findImages(@Arg('pointID') pointID: number) {
        return await Image.findBy({
            pointID: pointID
        });
    }

    @Query(() => DateTime)
    async getDateTime(@Arg('dateTimeID') dateTimeID: number) {
        return await DateTime.findOneBy({
            dateTimeID: dateTimeID
        });
    }
}