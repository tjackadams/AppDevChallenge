import * as actions from "../actions/actions";

function deviceReducer(state = {}, action) {
  switch (action.type) {
    case actions.UPDATED_DEVICE:
      return {
        ...state,
        [action.payload.deviceId]: {
          ...action.payload
        }
      };
    default:
      return state;
  }
}

export { deviceReducer };
