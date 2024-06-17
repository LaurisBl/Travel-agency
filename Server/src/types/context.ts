import { Request, Response } from 'express';
import { BaseUser } from '../schema';

interface Context {
    req: Request;
    res: Response;
    user: BaseUser | null;
}

export default Context;