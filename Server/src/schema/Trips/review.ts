import { Field, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";

@Entity()
@ObjectType()
export default class Review extends BaseEntity {
    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    reviewID!: number;

    @Field(() => Int!)
    @Column()
    tripID!: number;

    @Field(() => Int!)
    @Column()
    rating!: number;

    @Field(() => String!)
    @Column()
    description!: string;

    constructor() {
        super();

        this.reviewID = 0;
        this.tripID = 0;
        this.rating = 1;
        this.description = "None";
    }
}