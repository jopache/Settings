import { TreeNode } from '../treeNode';
import { Observable } from 'rxjs/Observable';

export interface TreeNodeSelector {
    activeNode: Observable<TreeNode>;
    setActiveNode(node: TreeNode): void;
}
