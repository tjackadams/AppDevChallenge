import React, { Component } from "react";

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
    this.getMapData();
  }

  getMapData = () => {
    setTimeout(this.getMapData, 30000);

    this.updateMap(this.props.data.Pins);
  };

  displayPin = (vDeviceId, vName, vLong, vLat, vText, vStatus, vImage) => {
    const random = Math.random();
    const icon = random > 0.8 ? RedIcon : random > 0.5 ? AmberIcon : GreenIcon;
    return {
      key: vDeviceId,
      icon: icon,
      name: vName,
      position: [vLat, vLong],
      text: vText
    };
  };

  updateMap = Pins => {
    let vPins = [];
    Pins.forEach(pin => {
      vPins[pin.deviceid] = this.displayPin(
        pin.deviceid,
        pin.name,
        pin.long,
        pin.lat,
        pin.text
      );
    });

    this.setState({
      vMapPins: vPins
    });
  };

  render() {
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
          subscriptionKey="MaBREwDQSRW830sXtzMaRjcaVGVOdmOeogSyKSTcbdc"
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

export default AlertMap;
