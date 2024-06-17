import log from 'fancy-log';
import 'reflect-metadata';
import express from 'express';
import { buildSchema } from 'type-graphql';
import cookieParser from 'cookie-parser';
import { ApolloServer } from 'apollo-server-express';
import * as asc from 'apollo-server-core'
import { connectToDB } from './utils/mysql';
import Context from './types/context';

async function bootstrap() {
    log("Building schema");

    // build the schema
    const schema = await buildSchema({
    });

    log("Initializing express");

    // init express
    const app = express();

    app.use(cookieParser());

    app.set('trust proxy', true)

    log("Initializing apollo server");

    // create the apollo server
    const server = new ApolloServer({
        schema,
        context: (ctx: Context) => {
            

            return ctx;
        },
        plugins: [
            asc.ApolloServerPluginLandingPageLocalDefault()
        ],
    });

    await server.start();

    log("Server started");
    log("Applying middleware to server");

    // apply middleware to server
    server.applyMiddleware({app});

    app.listen({port: 9049}, () => {
        log("App is listening on http://localhost:9049/graphql");
    });

    log("Connecting to DB");

    // connect to db
    await connectToDB();

    log("System fully initialized");
}

bootstrap().catch((err) => {
    log(err);
})