import { Field, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";
import BaseUser from "./baseUser";

@Entity()
@ObjectType()
export default class Admin extends BaseUser {
    @Field(() => Int)
    override type: number = this.getType();

    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    adminID!: number;

    setID(): void {
        this.ID = this.adminID;
    }

    getType(): number {
        return 3;
    }

    constructor() {
        super();

        this.adminID = 0;
        this.fName = "None";
        this.eMail = "None@email.com";
        this.passwordKey = "Nonexistent";
    }
}