import { Field, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";

@Entity()
@ObjectType()
export default class TripPoint extends BaseEntity {
    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    tripPointID!: number;

    @Field(() => Int!)
    @Column()
    pointID!: number;

    @Field(() => Int!)
    @Column()
    tripID!: number;

    constructor() {
        super();

        this.tripPointID = 0;
        this.pointID = 0;
        this.tripID = 0;
    }
}