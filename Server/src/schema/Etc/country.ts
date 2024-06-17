import { Field, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";

@Entity()
@ObjectType()
export default class Country extends BaseEntity {
    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    countryID!: number;

    @Field(() => String!)
    @Column()
    name!: string;

    constructor() {
        super();

        this.countryID = 0;
        this.name = "None";
    }
}