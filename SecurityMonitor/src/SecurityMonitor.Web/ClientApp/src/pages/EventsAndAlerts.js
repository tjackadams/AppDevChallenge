import React from "react";
import { Stack } from "office-ui-fabric-react/lib/Stack";

import { AlertMap } from "../components/AlertMap";

const EventsAndAlerts = () => (
  <Stack>
    <Stack.Item>
      <AlertMap />
    </Stack.Item>
  </Stack>
);

export default EventsAndAlerts;
