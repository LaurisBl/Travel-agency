import log from 'fancy-log';
import { DataSource } from 'typeorm';
import entities from '../schema';

export async function connectToDB() {
    try {
        const AppDataSource = new DataSource({
            type: "mysql",
            host: "localhost",
            port: 3307,
            username: "root",
            password: "root",
            database: "travel_agency",
            synchronize: false,
            logging: false,
            entities: entities
        })

        await AppDataSource.initialize();
        log("Connected to database");
    } catch (error) {
        log.error(error);
        process.exit(1);
    }
}