import React from 'react';
import LeftPanel from '../components/common/LeftPanel';

export default function HomeLayout() {
  return (
    <div className='d-flex'>
        <LeftPanel/>
        <h3>Right Panel</h3>
    </div>
  )
}
