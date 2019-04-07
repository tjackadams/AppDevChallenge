import React from "react";
import { Stack } from "office-ui-fabric-react/lib/Stack";

import { AlertMap } from "../components/AlertMap";

import Data from "./pindata";

const subscriptionKey = process.env.REACT_APP_AZUREMAPKEY;

const EventsAndAlerts = () => (
  <Stack>
    <Stack.Item>
      <AlertMap data={Data} subscriptionKey={subscriptionKey} />
    </Stack.Item>
  </Stack>
);

export default EventsAndAlerts;
