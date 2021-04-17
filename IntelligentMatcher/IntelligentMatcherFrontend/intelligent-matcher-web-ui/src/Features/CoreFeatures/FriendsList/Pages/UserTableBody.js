import React from "react";
import { Button, Table } from "semantic-ui-react";

const comparator = (a, b, userList, sortByColumn) => {
  if (userList[a][sortByColumn] > userList[b][sortByColumn]) return 1;
  if (userList[a][sortByColumn] < userList[b][sortByColumn]) return -1;
  if (userList[a][sortByColumn] === userList[b][sortByColumn]) return 0;
};

export default ({ userList, sortByColumn, sortDirection, history }) => {
  const generateRows = () => {
    const unsortedUserListKeys = Object.keys(userList);

    let sortedUserListKeys = unsortedUserListKeys;

    if (sortDirection === "ascending") {
      sortedUserListKeys = unsortedUserListKeys.sort((a, b) =>
        comparator(a, b, userList, sortByColumn)
      );
    } else if (sortDirection === "descending") {
      sortedUserListKeys = unsortedUserListKeys
        .sort((a, b) => comparator(a, b, userList, sortByColumn))
        .reverse();
    }

    console.log("sort", sortedUserListKeys);

    return sortedUserListKeys.map(key => (
      <Table.Row key={key}>
        <Table.Cell>{userList[key].firstName}</Table.Cell>
        <Table.Cell>{userList[key].lastName}</Table.Cell>
        <Table.Cell>{userList[key].email}</Table.Cell>
        <Table.Cell>{userList[key].age}</Table.Cell>
        <Table.Cell>
          <Button content="View" color="blue" onClick={() => viewUser(key)} />
        </Table.Cell>
      </Table.Row>
    ));
  };

  const viewUser = key => {
    history.push("/users/" + key);
  };

  return <Table.Body>{generateRows()}</Table.Body>;
};
