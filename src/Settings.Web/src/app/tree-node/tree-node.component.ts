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
  active = false;
  childApplicationName = '';

  constructor() { }

  onSelect(): void {
    this.treeNodeService.setActiveNode(this.node);
  }

  ngOnInit() {
    this.treeNodeService.activeNode.subscribe(treeNode => {
    if (this.node.id === treeNode.id) {
      console.log('setting active true. component node', this.node.name, 'selected node', treeNode.name);
      this.active = true;
    } else {
      this.active = false;
    }
    });
  }

  addChild(): void {
    this.treeNodeService.createChildNode(this.node.id, this.childApplicationName)
      .then(result => {
        this.childApplicationName = '';
      });
  }
}
