import EtcMutation from "./mutation/etcMutation";
import PointMutation from "./mutation/pointMutation";
import TransportationMutation from "./mutation/transportationMutation";
import TripMutation from "./mutation/tripMutation";
import UserMutation from "./mutation/userMutation";
import EtcQuery from "./query/etcQuery";
import PointQuery from "./query/pointQuery";
import TransportationQuery from "./query/transportationQuery";
import TripQuery from "./query/tripQuery";
import UserQuery from "./query/userQuery";

export const resolvers = [
    UserQuery,              UserMutation,
    TripQuery,              TripMutation,
    TransportationQuery,    TransportationMutation,
    PointQuery,             PointMutation,
    EtcQuery,               EtcMutation,
] as const;