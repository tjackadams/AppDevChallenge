import React, { Component } from "react";

import { Map as LeafletMap, Marker, TileLayer } from "react-leaflet";
import L from "leaflet";

import Data from "./pindata";

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
    this.getMapData();
  }

  getMapData = () => {
    window.setTimeout(this.getMapData, 30000);

    this.updateMap(Data);
  };

  updateMap = pins => {
    for (let index = 0; index < pins.Pins.length; index++) {
      const vPin = pins.Pins[index];
      this.displayPin(vPin);
    }
  };

  displayPin = pin => {
    this.setState(
      {
        vMapPins: this.state.vMapPins.map(el =>
          el.id === pin.deviceid ? { ...el, id: pin.deviceid } : el
        )
      },
      console.log("state", this.state.vMapPins)
    );
  };

  render() {
    const position = [this.state.center.lat, this.state.center.lng];
    const { vMapPins } = this.state;
    console.log(vMapPins);
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
          subscriptionKey="MaBREwDQSRW830sXtzMaRjcaVGVOdmOeogSyKSTcbdc"
        />
        {vMapPins &&
          vMapPins.map(vMapPin => <Marker position={vMapPin.position} />)}
      </LeafletMap>
    );
  }
}

export default AlertMap;
