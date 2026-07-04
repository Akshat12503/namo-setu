-- =============================================
-- NAMO SETU - Complete Database Schema
-- =============================================

-- USERS
CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    full_name VARCHAR(100) NOT NULL,
    email VARCHAR(150) UNIQUE NOT NULL,
    phone VARCHAR(15),
    password_hash TEXT NOT NULL,
    preferred_language VARCHAR(10) DEFAULT 'en',
    profile_image TEXT,
    is_verified BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT NOW()
);

-- TEMPLES
CREATE TABLE temples (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(200) NOT NULL,
    deity VARCHAR(100),
    location VARCHAR(200),
    city VARCHAR(100),
    state VARCHAR(100),
    latitude DECIMAL(10,8),
    longitude DECIMAL(11,8),
    description TEXT,
    significance TEXT,
    darshan_timings VARCHAR(200),
    dress_code VARCHAR(200),
    entry_fee DECIMAL(10,2) DEFAULT 0,
    image_url TEXT,
    rating DECIMAL(3,2) DEFAULT 0,
    total_reviews INT DEFAULT 0,
    is_featured BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT NOW()
);

-- ITINERARIES
CREATE TABLE itineraries (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID REFERENCES users(id) ON DELETE CASCADE,
    title VARCHAR(200),
    destination VARCHAR(200),
    start_date DATE,
    end_date DATE,
    num_travelers INT DEFAULT 1,
    budget DECIMAL(12,2),
    ai_generated_plan TEXT,
    is_saved BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT NOW()
);

-- BOOKINGS
CREATE TABLE bookings (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID REFERENCES users(id) ON DELETE CASCADE,
    booking_type VARCHAR(50) NOT NULL, -- 'travel', 'hotel', 'puja'
    reference_id UUID,
    status VARCHAR(50) DEFAULT 'pending', -- pending, confirmed, cancelled
    total_amount DECIMAL(12,2),
    payment_status VARCHAR(50) DEFAULT 'unpaid',
    booked_at TIMESTAMP DEFAULT NOW()
);

-- HOTELS / DHARAMSHALA
CREATE TABLE accommodations (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(200) NOT NULL,
    type VARCHAR(50), -- 'hotel', 'dharamshala', 'guesthouse'
    city VARCHAR(100),
    state VARCHAR(100),
    latitude DECIMAL(10,8),
    longitude DECIMAL(11,8),
    price_per_night DECIMAL(10,2),
    amenities TEXT,
    image_url TEXT,
    rating DECIMAL(3,2) DEFAULT 0,
    contact VARCHAR(50),
    created_at TIMESTAMP DEFAULT NOW()
);

-- PUJA SERVICES
CREATE TABLE puja_services (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    temple_id UUID REFERENCES temples(id) ON DELETE SET NULL,
    name VARCHAR(200) NOT NULL,
    description TEXT,
    price DECIMAL(10,2),
    duration_minutes INT,
    available_days VARCHAR(100),
    image_url TEXT,
    created_at TIMESTAMP DEFAULT NOW()
);

-- PUJA BOOKINGS
CREATE TABLE puja_bookings (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID REFERENCES users(id) ON DELETE CASCADE,
    puja_service_id UUID REFERENCES puja_services(id),
    scheduled_date DATE,
    devotee_name VARCHAR(100),
    gotra VARCHAR(100),
    special_wish TEXT,
    status VARCHAR(50) DEFAULT 'pending',
    amount DECIMAL(10,2),
    created_at TIMESTAMP DEFAULT NOW()
);

-- GROUP YATRA
CREATE TABLE group_yatras (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    created_by UUID REFERENCES users(id),
    group_name VARCHAR(200),
    destination VARCHAR(200),
    start_date DATE,
    end_date DATE,
    max_members INT,
    current_members INT DEFAULT 1,
    description TEXT,
    status VARCHAR(50) DEFAULT 'open',
    created_at TIMESTAMP DEFAULT NOW()
);

-- GROUP MEMBERS
CREATE TABLE group_yatra_members (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    group_id UUID REFERENCES group_yatras(id) ON DELETE CASCADE,
    user_id UUID REFERENCES users(id) ON DELETE CASCADE,
    role VARCHAR(50) DEFAULT 'member', -- 'admin', 'member'
    joined_at TIMESTAMP DEFAULT NOW()
);

-- REVIEWS
CREATE TABLE reviews (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID REFERENCES users(id) ON DELETE CASCADE,
    temple_id UUID REFERENCES temples(id) ON DELETE CASCADE,
    rating INT CHECK (rating BETWEEN 1 AND 5),
    comment TEXT,
    created_at TIMESTAMP DEFAULT NOW()
);

-- AI CHAT HISTORY
CREATE TABLE chat_messages (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID REFERENCES users(id) ON DELETE CASCADE,
    role VARCHAR(20) NOT NULL, -- 'user' or 'assistant'
    message TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT NOW()
);