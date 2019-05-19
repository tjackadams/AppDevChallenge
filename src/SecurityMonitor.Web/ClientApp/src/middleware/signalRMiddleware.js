import {
  JsonHubProtocol,
  HttpTransportType,
  HubConnectionBuilder,
  LogLevel
} from "@aspnet/signalr";

import * as actions from "../actions/actions";

const startSignalRConnection = connection =>
  connection
    .start()
    .then(() => console.info("SignalR Connected"))
    .catch(err => console.error("SignalR Connection Error: ", err));

const urlRoot = "http://localhost:51151";
const alarmHub = `${urlRoot}/hub/notifications`;

const protocol = new JsonHubProtocol();

const transport = HttpTransportType.WebSockets | HttpTransportType.LongPolling;

const options = {
  transport,
  logMessageContent: true,
  logger: LogLevel.Trace
};

const connection = new HubConnectionBuilder()
  .withUrl(alarmHub, options)
  .withHubProtocol(protocol)
  .build();

connection.onclose(() => setTimeout(startSignalRConnection(connection), 5000));

startSignalRConnection(connection);

const signalRMiddleware = store => next => async action => {
  console.log("middleware invoked.");

  return next(action);
};

const signalRCommands = store => {
  connection.on("updatedDevice", res => {
    store.dispatch({
      type: actions.UPDATED_DEVICE,
      payload: res
    });
  });
};

export { signalRMiddleware, signalRCommands };
