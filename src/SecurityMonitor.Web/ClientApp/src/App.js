import React, { Component } from "react";

import EventsAndAlerts from "./pages/EventsAndAlerts";
import { Layout } from "./components/Layout";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <EventsAndAlerts />
      </Layout>
    );
  }
}
