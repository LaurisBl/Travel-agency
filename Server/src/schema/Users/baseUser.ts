import { Field, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";

@ObjectType()
export default abstract class BaseUser extends BaseEntity {
    @Field(() => Int)
    ID: number = 0;

    @Field(() => Int)
    abstract type: number;

    @Field(() => String)
    @Column()
    fName!: string;

    @Field(() => String)
    @Column()
    eMail!: string;

    @Field(() => String)
    @Column()
    passwordKey!: string;

    abstract setID(): void;

    constructor() {
        super();

        this.fName = "None";
        this.eMail = "None@email.com";
        this.passwordKey = "Nonexistent";
    }
}