import React from "react";
import { Stack } from "office-ui-fabric-react/lib/Stack";
import { AlarmContainer, MapContainer } from "../containers";

const EventsAndAlerts = () => (
  <Stack horizontal tokens={{ childrenGap: 10 }}>
    <Stack.Item>
      <MapContainer />
    </Stack.Item>
    <Stack.Item>
      <AlarmContainer />
    </Stack.Item>
  </Stack>
);

export default EventsAndAlerts;
