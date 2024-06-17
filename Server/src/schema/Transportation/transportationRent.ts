import { Field, Float, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";

@Entity()
@ObjectType()
export default class TransportationRent extends BaseEntity {
    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    transportationRentID!: number;

    @Field(() => Int!)
    @Column()
    countryID!: number;

    @Field(() => Int!)
    @Column()
    transportationTypeID!: number;

    @Field(() => String!)
    @Column()
    companyName!: String;

    @Field(() => Float)
    @Column()
    price!: number;

    constructor() {
        super();

        this.transportationRentID = 0;
        this.countryID = 0;
        this.transportationTypeID = 0;
        this.companyName = "None";
    }
}