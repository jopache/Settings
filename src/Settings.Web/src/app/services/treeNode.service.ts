import { TreeNode } from '../treenode';
import { Observable } from 'rxjs/Observable';

export interface TreeNodeService {
    activeNode: Observable<TreeNode>;
    setActiveNode(node: TreeNode): void;
    createChildNode(parentId: number, nodeName: string): Promise<any>;
}
