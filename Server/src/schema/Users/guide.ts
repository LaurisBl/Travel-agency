import { Field, InputType, Int, ObjectType } from "type-graphql";
import { BaseEntity, Column, Entity, PrimaryGeneratedColumn } from "typeorm";
import BaseUser from "./baseUser";

@Entity()
@ObjectType()
export default class Guide extends BaseUser {
    @Field(() => Int)
    override type: number = this.getType();

    @Field(() => Int!)
    @PrimaryGeneratedColumn()
    guideID!: number;

    @Field(() => String)
    @Column({default: ""})
    phone: string = "";

    setID(): void {
        this.ID = this.guideID;
    }

    getType(): number {
        return 1;
    }

    constructor() {
        super();

        this.guideID = 0;
        this.fName = "None";
        this.eMail = "None@email.com";
        this.passwordKey = "None";
    }
}