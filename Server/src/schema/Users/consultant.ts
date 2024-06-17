import { Field, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";
import BaseUser from "./baseUser";

@Entity()
@ObjectType()
export default class Consultant extends BaseUser {
    @Field(() => Int)
    override type: number = this.getType();

    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    consultantID!: number;

    @Field(() => String)
    @Column({default: ""})
    phone: string = "";

    setID(): void {
        this.ID = this.consultantID;
    }

    getType(): number {
        return 2;
    }

    constructor() {
        super();

        this.consultantID = 0;
        this.fName = "None";
        this.eMail = "None@email.com";
        this.passwordKey = "None";
    }
}