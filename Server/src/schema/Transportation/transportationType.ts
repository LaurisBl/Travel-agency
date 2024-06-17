import { Field, Float, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";

@Entity()
@ObjectType()
export default class TransportationType extends BaseEntity {
    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    transportationTypeID!: number;

    @Field(() => String!)
    @Column()
    name!: String;

    constructor() {
        super();

        this.transportationTypeID = 0;
        this.name = "None";
    }
}