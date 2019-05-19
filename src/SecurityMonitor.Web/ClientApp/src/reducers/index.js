import { combineReducers } from "redux";
import { deviceReducer } from "./reducer";

// main reducers
export const reducers = combineReducers({
  devices: deviceReducer
});
