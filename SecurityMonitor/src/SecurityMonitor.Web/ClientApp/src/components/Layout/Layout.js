import React from "react";
import { Stack } from "office-ui-fabric-react/lib/Stack";

import { Header } from "../Header";

const Layout = ({ children }) => (
  <Stack>
    <Header />
    <Stack.Item>{children}</Stack.Item>
  </Stack>
);

export default Layout;
