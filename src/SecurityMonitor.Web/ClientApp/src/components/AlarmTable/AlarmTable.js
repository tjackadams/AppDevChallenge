import React, { PureComponent } from "react";
import {
  DetailsList,
  DetailsListLayoutMode,
  IColumn
} from "office-ui-fabric-react/lib/DetailsList";
import { Icon } from "office-ui-fabric-react/lib/Icon";
import { Link } from "office-ui-fabric-react/lib/Link";
import { mergeStyleSets } from "office-ui-fabric-react/lib/Styling";

const alarms = [
  {
    deviceId: 1,
    name: "Alarm1",
    status: 1
  },
  {
    deviceId: 2,
    name: "Alarm2",
    status: 2
  }
];

const classes = mergeStyleSets({
  severityCell: {
    textAlign: "center"
  },
  severityHeader: {
    textAlign: "center"
  },
  badgeWarning: {
    color: "#E7B416"
  },
  badgeDanger: {
    color: "#CC3232"
  }
});

class AlarmTable extends PureComponent {
  constructor(props) {
    super(props);

    const columns: IColumn[] = [
      {
        key: "column1",
        name: "Name",
        fieldName: "name",
        data: "string"
      },
      {
        key: "column2",
        name: "Severity",
        className: classes.severityCell,
        headerClassName: classes.severityHeader,
        fieldName: "status",
        data: "number",
        onRender: item => {
          console.log("item", item);
          return item.status === 1 ? (
            <Icon iconName={"CircleFill"} className={classes.badgeWarning} />
          ) : (
            <Icon iconName={"CircleFill"} className={classes.badgeDanger} />
          );
        }
      },
      {
        key: "column3",
        onRender: item => {
          return (
            <div
              style={{
                display: "flex",
                justifyContent: "space-between",
                alignItems: "center",
                alignContent: "center"
              }}
            >
              <Link onClick={() => console.log("you clicked!")}>dismiss</Link>
              <Link>alert</Link>
            </div>
          );
        }
      }
    ];

    this.state = {
      columns: columns
    };
  }

  render() {
    const { columns } = this.state;
    return (
      <DetailsList
        items={alarms}
        columns={columns}
        compact={true}
        layoutMode={DetailsListLayoutMode.justified}
      />
    );
  }
}

export default AlarmTable;
