import { compose, createStore, applyMiddleware } from "redux";
import { createLogger } from "redux-logger";

import { reducers } from "../reducers";
import {
  signalRMiddleware,
  signalRCommands
} from "../middleware/signalRMiddleware";

const logger = createLogger({
  collasped: true
});

const initialState = {
  devices: {},
  alarms: {}
};

let middlewares = [logger, signalRMiddleware];

let middleware = applyMiddleware(...middlewares);

if (
  process.env.NODE_ENV !== "production" &&
  window.__REDUX_DEVTOOLS_EXTENSION__
) {
  middleware = compose(
    middleware,
    window.__REDUX_DEVTOOLS_EXTENSION__()
  );
}

const store = createStore(reducers, initialState, middleware);

signalRCommands(store);

export { store };
