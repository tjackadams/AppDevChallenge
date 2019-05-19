import React from "react";
import { Stack } from "office-ui-fabric-react/lib/Stack";

import MapContainer from "../containers/MapContainer";

const EventsAndAlerts = () => (
  <Stack>
    <Stack.Item>
      <MapContainer />
    </Stack.Item>
  </Stack>
);

export default EventsAndAlerts;
