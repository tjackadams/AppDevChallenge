import React, { Component } from "react";
import PropTypes from "prop-types";
import { Map as LeafletMap, Marker, Popup, TileLayer } from "react-leaflet";
import L from "leaflet";

import "./style.css";

const GreenIcon = new L.Icon({
  iconUrl:
    "https://cdn.rawgit.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-green.png",
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34]
});

var RedIcon = new L.Icon({
  iconUrl:
    "https://cdn.rawgit.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-red.png",
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34]
});

var AmberIcon = new L.Icon({
  iconUrl:
    "https://cdn.rawgit.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-orange.png",
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34]
});

class AlertMap extends Component {
  state = {
    center: {
      lat: 51.505,
      lng: -1
    },
    vMapPins: []
  };

  componentDidMount() {
    this.updateMap(this.props.data);
  }

  componentDidUpdate(prevProps, prevState) {
    if (prevProps.data !== this.props.data) {
      this.updateMap(this.props.data);
    }
  }

  displayPin = (vDeviceId, vName, vLong, vLat, vStatus) => {
    const icon =
      vStatus === 2 ? RedIcon : vStatus === 1 ? AmberIcon : GreenIcon;
    return {
      key: vDeviceId,
      icon: icon,
      name: vName,
      position: [vLat, vLong],
      text: "test"
    };
  };

  updateMap = Pins => {
    const displayPins = Object.values(Pins).map(pin => {
      return this.displayPin(
        pin.deviceId,
        pin.name,
        pin.longitude,
        pin.latitude,
        pin.status
      );
    });

    this.setState({
      vMapPins: displayPins
    });
  };

  render() {
    console.log("pins", this.state.vPins);
    const position = [this.state.center.lat, this.state.center.lng];
    return (
      <LeafletMap
        center={position}
        zoom={7.5}
        maxZoom={10}
        attributionControl={true}
        zoomControl={true}
        doubleClickZoom={true}
        scrollWheelZoom={true}
        dragging={true}
        animate={true}
        easeLinearity={0.35}
      >
        <TileLayer
          url="https://atlas.microsoft.com/map/tile/png?api-version=1&layer=basic&style=main&tileSize=512&zoom={z}&x={x}&y={y}&subscription-key={subscriptionKey}"
          maxZoom={18}
          tileSize={512}
          zoomOffset={-1}
          id="azuremaps.road"
          crossOrigin={true}
          subscriptionKey={this.props.subscriptionKey}
        />
        {this.state.vMapPins.map(pin => {
          return (
            <Marker key={pin.key} position={pin.position} icon={pin.icon}>
              <Popup>
                {pin.name} <br /> {pin.text}
              </Popup>
            </Marker>
          );
        })}
      </LeafletMap>
    );
  }
}

AlertMap.propTypes = {
  subscriptionKey: PropTypes.string.isRequired
};

export default AlertMap;
