import Long from "long";
import { Field, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";

@Entity()
@ObjectType()
export default class DateTime extends BaseEntity {
    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    dateTimeID!: number;

    @Field(() => String!)
    @Column()
    timeStamp!: string;

    constructor() {
        super();

        this.dateTimeID = 0;
        this.timeStamp = "0";
    }
}