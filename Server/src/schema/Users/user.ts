import { Field, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";
import BaseUser from "./baseUser";

@Entity()
@ObjectType()
export default class User extends BaseUser {
    @Field(() => Int)
    override type: number = this.getType();

    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    userID!: number;

    @Field(() => String)
    @Column({default: ""})
    phone: string = "";

    getType(): number {
        return 0;
    }

    setID(): void {
        this.ID = this.userID;
    }

    constructor() {
        super();

        this.userID = 0;
        this.fName = "None";
        this.eMail = "None@email.com";
        this.passwordKey = "Nonexistent";
    }
}