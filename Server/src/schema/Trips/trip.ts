import { Field, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn, Timestamp } from "typeorm";

@Entity()
@ObjectType()
export default class Trip extends BaseEntity {
    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    tripID!: number;

    @Field(() => Int!)
    @Column()
    userID!: number;

    @Field(() => String!)
    @Column()
    name!: string;

    @Field(() => Int!)
    @Column()
    guideID!: number;

    @Field(() => Int!)
    @Column()
    consultantID!: number;

    @Field(() => Int!)
    @Column()
    dateTimeID!: number;

    @Field(() => Int!)
    @Column()
    transportRentID!: number;

    constructor() {
        super();

        this.tripID = 0;
        this.userID = 0;
        this.name = "None";
        this.guideID = 0;
        this.consultantID = 0;
        this.dateTimeID = 0;
        this.transportRentID = 0;
    }
}