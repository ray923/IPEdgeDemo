// @ts-nocheck
import cx from 'classnames';
import React, { Component } from 'react';

export default class ImageCarousel extends Component {
    constructor(props) {
        super(props);
        this.state = { items: [], text: '', todo: 0};
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleDone = this.handleDone.bind(this);
    }


    render() {
        return (
            <>
                <div>
                    <h2>
                        Todo List
                    </h2>
                    <form onSubmit={this.handleSubmit}>
                        <input
                            id="new-todo"
                            onChange={this.handleChange}
                            value={this.state.text}
                        />
                        <button>
                            Add
                        </button>
                        <li>{this.state.todo} remaining out of {this.state.items.length} tasks</li>
                    </form>
                    <ul>
                        {this.state.items.map(item => (
                        <li key={item.id} onClick={() => this.handleDone(item.id)} class={item.done ? 'is-done' : ''}>{item.text}</li>
                        ))}
                    </ul>
                </div>
                <style>{`
                    .is-done {
                        text-decoration: line-through;
                    }
                `}</style>
            </>
        );
    }

    handleChange(e) {
        this.setState({ text: e.target.value });
    }

    handleSubmit(e) {
        e.preventDefault();
        if (this.state.text.length === 0) {
            return;
        }
        const newItem = {
            text: this.state.text,
            id: Date.now(),
            done: false
        };
        this.setState(state => ({
            items: state.items.concat(newItem),
            todo: state.todo + 1,
            text: ''
        }));
    }

    handleDone(id) {
        var newItems = this.state.items.map(item => {
            if (item.id === id)
            {
                item.done = !item.done;
            }
            return item;
        });
        var total = 0;
        this.state.items.map(item => {
            if(item.done === false)
            {
                total += 1;
            }
            else{}
       })
        this.setState({items: newItems, todo: total});
    }
}
