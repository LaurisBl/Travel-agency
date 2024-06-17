import { Field, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";

@Entity()
@ObjectType()
export default class TravelParty extends BaseEntity {
    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    travelPartyID!: number;

    @Field(() => Int!)
    @Column()
    tripID!: number;

    @Field(() => Int!)
    @Column()
    amount!: number;

    constructor() {
        super();

        this.travelPartyID = 0;
        this.tripID = 0;
        this.amount = 0;
    }
}