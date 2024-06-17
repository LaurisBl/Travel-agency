import { Field, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";

@Entity()
@ObjectType()
export default class Image extends BaseEntity {
    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    imageID!: number;

    @Field(() => Int!)
    @Column()
    pointID!: number;

    @Field(() => String!)
    @Column()
    imageLink!: string;

    constructor() {
        super();

        this.imageID = 0;
        this.pointID = 1;
        this.imageLink = "None";
    }
}