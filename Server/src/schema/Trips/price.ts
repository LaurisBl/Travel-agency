import { Field, Float, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";

@Entity()
@ObjectType()
export default class Price extends BaseEntity {
    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    priceID!: number;

    @Field(() => Int!)
    @Column()
    tripID!: number;

    @Field(() => Float!)
    @Column()
    price!: number;

    constructor() {
        super();

        this.priceID = 0;
        this.tripID = 0;
        this.price = 0;
    }
}