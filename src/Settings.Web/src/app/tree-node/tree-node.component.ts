import { TreeNodeService } from '../services/treeNode.service';
import { Component, OnInit, Input} from '@angular/core';
import { TreeNode } from '../treenode';

@Component({
  selector: 'app-tree-node',
  templateUrl: './tree-node.component.html',
  styleUrls: ['./tree-node.component.scss']
})
export class TreeNodeComponent implements OnInit {
  @Input() node: TreeNode;
  @Input() treeNodeService: TreeNodeService;
  @Input() nodeLabel: String;
  active = false;
  childName = '';
  showChildren = false;
  showAddChild = false;

  constructor() { }

  onSelect(): void {
    this.treeNodeService.setActiveNode(this.node);
  }

  ngOnInit() {
    console.log('tree node init');
    this.treeNodeService.activeNode.subscribe(treeNode => {
    if (treeNode != null
      && this.node.id === treeNode.id) {
      this.active = true;
    } else {
      this.active = false;
    }
    });
  }

  addChild(): void {
    this.treeNodeService.createChildNode(this.node.id, this.childName)
      .then(result => {
        const childNode = result as TreeNode;
        childNode.aggregatePermissions = this.node.aggregatePermissions;
        this.node.children.push(childNode);
        this.childName = '';
        this.showChildren = true;
        this.showAddChild = false;
      });
  }
}
