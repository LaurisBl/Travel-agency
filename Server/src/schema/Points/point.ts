import { Field, Float, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";

@Entity()
@ObjectType()
export default class Point extends BaseEntity {
    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    pointID!: number;

    @Field(() => Int!)
    @Column()
    pointTypeID!: number;

    @Field(() => Int!)
    @Column()
    cityID!: number;

    @Field(() => String!)
    @Column()
    name!: string;

    @Field(() => String!)
    @Column()
    address!: string;

    @Field(() => Float!)
    @Column()
    price!: number;

    constructor() {
        super();

        this.pointID = 0;
        this.pointTypeID = 0;
        this.cityID = 0;
        this.name = "None";
        this.address = "None";
        this.price = 0;
    }
}