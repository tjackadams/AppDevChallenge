import React from "react";
import { Stack } from "office-ui-fabric-react/lib/Stack";

import { AlertMap } from "../components/AlertMap";

import Data from "./pindata";

const EventsAndAlerts = () => (
  <Stack>
    <Stack.Item>
      <AlertMap data={Data} />
    </Stack.Item>
  </Stack>
);

export default EventsAndAlerts;
