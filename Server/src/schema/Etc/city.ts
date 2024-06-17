import { Field, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";

@Entity()
@ObjectType()
export default class City extends BaseEntity {
    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    cityID!: number;

    @Field(() => Int!)
    @Column()
    countryID!: number;

    @Field(() => String!)
    @Column()
    name!: string;

    constructor() {
        super();

        this.cityID = 0;
        this.countryID = 0;
        this.name = "None";
    }
}