import { Field, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";

@Entity()
@ObjectType()
export default class Baggage extends BaseEntity {
    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    baggageID!: number;

    @Field(() => Int!)
    @Column()
    tripID!: number;

    @Field(() => Int!)
    @Column()
    transportTypeID!: number;

    @Field(() => Int!)
    @Column()
    amount!: number;

    @Field(() => Int!)
    @Column()
    weight!: number;

    constructor() {
        super();

        this.baggageID = 0;
        this.tripID = 0;
        this.transportTypeID = 0;
        this.amount = 1;
        this.weight = 1;
    }
}