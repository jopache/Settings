export class TreeNode {
  name: string;
  id: number;
  children: TreeNode[];
  aggregatePermissions: PermissionsAggregateModel;
}

export class PermissionsAggregateModel {
  canReadAtAnyRelatedNode: boolean;
  canWriteAtAnyRelatedNode: boolean;
  canDecryptAtAnyRelatedNode: boolean;
  canAddChildrenAtAnyRelatedNode: boolean;
  permissions: PermissionModel[];
}

export class PermissionModel {
  canRead: boolean;
  canWrite: boolean;
  canDecrypt: boolean;
  canAddChildren: boolean;
  applicationId: number;
}
