import { Field, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";

@Entity()
@ObjectType()
export default class PointType extends BaseEntity {
    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    pointTypeID!: number;

    @Field(() => String!)
    @Column()
    pointTypeName!: string;

    constructor() {
        super();

        this.pointTypeID = 0;
        this.pointTypeName = "None";
    }
}