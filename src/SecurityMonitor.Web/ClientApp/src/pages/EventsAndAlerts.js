import React from "react";
import { Stack } from "office-ui-fabric-react/lib/Stack";

import AlarmsContainer from "../containers/AlarmsContainer";

const EventsAndAlerts = () => (
  <Stack>
    <Stack.Item>
      <AlarmsContainer />
    </Stack.Item>
  </Stack>
);

export default EventsAndAlerts;
