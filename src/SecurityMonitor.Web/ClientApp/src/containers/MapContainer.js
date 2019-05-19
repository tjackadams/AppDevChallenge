import React, { Component } from "react";
import { ErrorBoundary } from "react-error-boundary";
import { connect } from "react-redux";

import { AlertMap } from "../components/AlertMap";

const baseUrl = process.env.REACT_APP_APIURL;
const subscriptionKey = process.env.REACT_APP_AZUREMAPKEY;

const MyFallbackComponent = ({ componentStack, error }) => (
  <div>
    <p>
      <strong>Oops! An error occured!</strong>
    </p>
    <p>Here’s what we know…</p>
    <p>
      <strong>Error:</strong> {error.toString()}
    </p>
    <p>
      <strong>Stacktrace:</strong> {componentStack}
    </p>
  </div>
);

class MapContainer extends Component {
  state = {
    devices: this.props.devices
  };

  componentDidUpdate(prevProps, prevState) {
    if (prevProps.devices !== this.props.devices) {
      this.setState({
        devices: this.props.devices
      });
    }
  }

  render() {
    return (
      <ErrorBoundary FallbackComponent={MyFallbackComponent}>
        <AlertMap data={this.state.devices} subscriptionKey={subscriptionKey} />
      </ErrorBoundary>
    );
  }
}

const mapStateToProps = state => {
  return {
    devices: state.devices
  };
};

export default connect(mapStateToProps)(MapContainer);
